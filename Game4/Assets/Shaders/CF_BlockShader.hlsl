
void DitherSome_float(float2 uv, out float val)
{
#if defined(SHADERGRAPH_PREVIEW)

    val = 0.25f;
#else

    /*int row = (int)(uv.y * 4);
    int col = (int)(uv.x * 4);

    if (col % 2 == 1)
        val = 1;
    if (row % 2 == 1)
        val = 1;

    val = 0.25f;*/

   /* int row = (int)(uv.y * 4);
    int col = (int)(uv.x * 4);

    if ((col + row) % 2 == 0)
        val = 0.25f;

    val = 1;
    */

    int row = (int)(uv.y * 5);
    int col = (int)(uv.x * 5);

    if (row == 0 && col == 0 || row == 4 && col == 0 || row == 0 && col == 4 || row == 4 && col == 4)
        val = 1;

    if ((col + row) % 2 == 0)
        val = 0.25f;

    if (row == 0 || row == 4)
        val = 0.25f;

    if (col == 0 || col == 4)
        val = 0.25f;

    val = 1;

    

#endif
}

#endif //MYHLSLINCLUDE_INCLUDED

/*
float DitherSome(float2 uv)
{
    int row = (int)(uv.y * 4);
    int col = (int)(uv.x * 4);

    if (col % 2 == 1)
        return 1;
    if (row % 2 == 1)
        return 1;

    return 0.25f;
}

float DitherHalf(float2 uv)
{
    int row = (int)(uv.y * 4);
    int col = (int)(uv.x * 4);
    if ((col + row) % 2 == 0)
        return 0.25f;
    return 1;

}

float DitherWhole(float2 uv)
{
    int row = (int)(uv.y * 5);
    int col = (int)(uv.x * 5);
    if (row == 0 && col == 0 || row == 4 && col == 0 || row == 0 && col == 4 || row == 4 && col == 4)
        return 1;
    if ((col + row) % 2 == 0)
        return 0.25f;
    if (row == 0 || row == 4)
        return 0.25f;
    if (col == 0 || col == 4)
        return 0.25f;
    return 1;

}

  fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                if (_Dither > 0 && _Dither <= 1)
                    col *= DitherSome(i.uv);
                if (_Dither > 1 && _Dither <= 2)
                    col *= DitherHalf(i.uv);
                if (_Dither > 2 && _Dither <= 3)
                    col *= (1.25f - DitherSome(i.uv));

*/

