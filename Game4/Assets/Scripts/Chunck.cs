using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunck : MonoBehaviour
{
    public GameObject tile;

    private PhotonView PV;
    private bool isInit = false;

    struct WearBlockData
    {
        public int i;
        public float w;
    }


    List<int> brokenBlocks = new List<int>();
    List<WearBlockData> wornBlocks = new List<WearBlockData>();

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        // checks if we have done this before
        if (isInit)
            return;
        isInit = true;

        // photon component
        PV = GetComponent<PhotonView>();

        // generates level
        LevelGenerator level = LevelGenerator.self;
        float scale = tile.transform.localScale.x;

        for (int i = -level.width / 2; i < level.width / 2; i++)
        {
            for (int j = -level.height / 2; j < level.height / 2; j++)
            {
                Vector2 pos = (Vector2)transform.position + new Vector2(i, j) * scale;
                createWallAt(pos);
            }
        }

        // breaks the blocks 
        while (brokenBlocks.Count > 0)
        {
            BreakChild(brokenBlocks[0]);
            brokenBlocks.RemoveAt(0);
        }

        // breaks the blocks 
        while (wornBlocks.Count > 0)
        {
            WearChild(wornBlocks[0].i, wornBlocks[0].w);
            brokenBlocks.RemoveAt(0);
        }
    }

    void createWallAt(Vector2 pos)
    {
        LevelGenerator level = LevelGenerator.self;

        if (pos.sqrMagnitude > level.radiusOfCentreArea)
        {
            Instantiate(tile, pos, Quaternion.identity, transform);
        }
    }

    public void WearChild(int index, float wear)
    {
        PV.RPC("WearChildRPC", RpcTarget.OthersBuffered, index, wear);
    }

    [PunRPC]
    public void WearChildRPC(int index, float wear)
    {
        if (isInit)
        {
            transform.GetChild(index).GetComponent<Wall>().Wear(wear);
        }
        else
        {
            WearBlockData d;
            d.i = index;
            d.w = wear;
            wornBlocks.Add(d);
        }
    }

    public void BreakChild(int index)
    {
        PV.RPC("BreakChildRPC", RpcTarget.AllBufferedViaServer, index);
    }

    [PunRPC]
    void BreakChildRPC(int index)
    {
        if (isInit)
        {
            transform.GetChild(index).GetComponent<Wall>().Break();
        }
        else
        {
            brokenBlocks.Add(index);
        }
    }
}
