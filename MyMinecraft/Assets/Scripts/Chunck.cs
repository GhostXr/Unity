using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]

public class Chunck : MonoBehaviour {

    public Block[,,] map;
    public static int width = 30, height = 20;
    public int rowNum = 0;
    public int lineNum = 0;

    int ChunckId = 0;
    
    public static List<Chunck> Chuncks = new List<Chunck>();

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangulos = new List<int>();
    List<Vector2> uvs = new List<Vector2>();
    List<Color> color = new List<Color>();

    float TextureOffset = 1f/16f;
    Mesh mesh;

    public static bool working = false;
    bool ready = false;

    void Start()
    {
        Chuncks.Add(this);
    }

	// Use this for initialization
	void Update ()
    {
        if (working == false && ready == false)
        {
            ready = true;
            startFunction();
        }
    }

    public void startFunction()
    {
        working = true;
        mesh = new Mesh();
        map = new Block[width, height, width];

        this.StartCoroutine(CalculateMap());
    }

    public static int seed = 5015;
    public IEnumerator CalculateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    map[x, y, z] = GetTheoreticalBlock(transform.position + new Vector3(x, y, z));
                }
            }
        }
        yield return 0;

        this.StartCoroutine(CalculateMesh());
    }

    public IEnumerator CalculateMesh()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {
                    if (map[x, y, z] != null)
                    {
                        if (y < 1) continue;
                        if (isBlockTransparent(x, y, z+1))
                        {
                            AddCubeFront(x, y, z);
                        }
                        if (isBlockTransparent(x, y, z - 1))
                        {
                            AddCubeBack(x, y, z);
                        }
                        if (isBlockTransparent(x, y + 1, z))
                        {
                            AddCubeTop(x, y, z);
                        }
                        if (isBlockTransparent(x, y - 1, z))
                        {
                            AddCubeBottom(x, y, z);
                        }
                        if (isBlockTransparent(x + 1, y, z))
                        {
                            AddCubeRight(x, y, z);
                        }
                        if (isBlockTransparent(x - 1, y, z))
                        {
                           AddCubeLeft(x, y, z);
                        }
                    }
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangulos.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.colors = color.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        MeshUtility.Optimize(mesh);

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;

        yield return 0;

        working = false;
        //ChunckId++;

        //Chuncks[ChunckId].startFunction();
    }
    
    public Block GetTheoreticalBlock(Vector3 pos)
    {
        UnityEngine.Random.seed = seed;

        Vector3 offset = new Vector3(UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000);

        float noiseX = Mathf.Abs((float)(pos.x + offset.x) / 20);
        float noiseY = Mathf.Abs((float)(pos.y + offset.y) / 20);
        float noiseZ = Mathf.Abs((float)(pos.z + offset.z) / 20);

        float noiseValue = SimplexNoise.Noise.Generate(noiseX, noiseY, noiseZ);

        noiseValue += (8 - (float)pos.y) / 5;
        noiseValue /= (float)pos.y / 4f;


        if (noiseValue > 0.2)
        {
            return Block.getBlock("Dirt");
        }
        return null;
    }

    bool isBlockTransparent(int x, int y, int z)
    {
        if (x >= width || y >= height || z >= width || x < 0 || y < 0 || z < 0)
        {
            if (GetTheoreticalBlock(transform.position + new Vector3(x, y, z)) == null)
                return true;
            else
                return false;
        }
        if (map[x, y, z] == null)
        {
            return true;
        }

        return false;
    }

    public void AddCubeFront(int x, int y, int z)
    {
       // x = x + Mathf.FloorToInt(transform.position.x);
       // y = y + Mathf.FloorToInt(transform.position.y);
       // z = z + Mathf.FloorToInt(transform.position.z);

        z++;

        int offset = 1;
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        triangulos.Add(4 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 0, z + 0));  //1
        vertices.Add(new Vector3(x + -1, y + 0, z + 0));  //2
        vertices.Add(new Vector3(x + -1, y + 1, z + 0));  //3
        vertices.Add(new Vector3(x + 0, y + 1, z + 0));  //4
    }

    public void AddCubeBack(int x, int y, int z)
    {
        // x = x + Mathf.FloorToInt(transform.position.x);
        // y = y + Mathf.FloorToInt(transform.position.y);
        // z = z + Mathf.FloorToInt(transform.position.z);

        int offset = 1;
        triangulos.Add(1 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);

        triangulos.Add(1 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(4 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 0, z + 0));  //1
        vertices.Add(new Vector3(x + -1, y + 0, z + 0));  //2
        vertices.Add(new Vector3(x + -1, y + 1, z + 0));  //3
        vertices.Add(new Vector3(x + 0, y + 1, z + 0));  //4
    }

    public void AddCubeTop(int x, int y, int z)
    {
        // x = x + Mathf.FloorToInt(transform.position.x);
        // y = y + Mathf.FloorToInt(transform.position.y);
        // z = z + Mathf.FloorToInt(transform.position.z);

        int offset = 1;
        triangulos.Add(1 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);

        triangulos.Add(1 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(4 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 1, z + 0));  //1
        vertices.Add(new Vector3(x + -1, y + 1, z + 0));  //2
        vertices.Add(new Vector3(x + -1, y + 1, z + 1));  //3
        vertices.Add(new Vector3(x + 0, y + 1, z + 1));  //4
    }

    public void AddCubeBottom(int x, int y, int z)
    {
        // x = x + Mathf.FloorToInt(transform.position.x);
        // y = y + Mathf.FloorToInt(transform.position.y);
        // z = z + Mathf.FloorToInt(transform.position.z);

        int offset = 1;
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        triangulos.Add(4 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 0, z + 0));  //1
        vertices.Add(new Vector3(x + -1, y + 0, z + 0));  //2
        vertices.Add(new Vector3(x + -1, y + 0, z + 1));  //3
        vertices.Add(new Vector3(x + 0, y + 0, z + 1));  //4
    }

    public void AddCubeRight(int x, int y, int z)
    {
        // x = x + Mathf.FloorToInt(transform.position.x);
        // y = y + Mathf.FloorToInt(transform.position.y);
        // z = z + Mathf.FloorToInt(transform.position.z);

        int offset = 1;
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        triangulos.Add(4 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 0, z + 0));  //1
        vertices.Add(new Vector3(x + 0, y + 0, z + 1));  //2
        vertices.Add(new Vector3(x + 0, y + 1, z + 1));  //3
        vertices.Add(new Vector3(x + 0, y + 1, z + 0));  //4
    }

    public void AddCubeLeft(int x, int y, int z)
    {
        // x = x + Mathf.FloorToInt(transform.position.x);
        // y = y + Mathf.FloorToInt(transform.position.y);
        // z = z + Mathf.FloorToInt(transform.position.z);

        x--;

        int offset = 1;
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(2 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        triangulos.Add(4 - offset + vertices.Count);
        triangulos.Add(3 - offset + vertices.Count);
        triangulos.Add(1 - offset + vertices.Count);

        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum));
        uvs.Add(new Vector2(TextureOffset * rowNum + TextureOffset, TextureOffset * lineNum + TextureOffset));
        uvs.Add(new Vector2(TextureOffset * rowNum, TextureOffset * lineNum + TextureOffset));

        vertices.Add(new Vector3(x + 0, y + 0, z + 1));  //1
        vertices.Add(new Vector3(x + 0, y + 0, z + 0));  //2
        vertices.Add(new Vector3(x + 0, y + 1, z + 0));  //3
        vertices.Add(new Vector3(x + 0, y + 1, z + 1));  //4
    }

    public static Chunck GetChunck(int x, int y, int z)
    {
        for (int i = 0; i < Chuncks.Count; i++)
        {
            Vector3 pos = new Vector3(x, y, z);
            Vector3 cpos = Chuncks[i].transform.position;

            if (cpos.Equals(pos))
            {
                return Chuncks[i];
            }

            if (pos.x < cpos.x || pos.y < cpos.y || pos.z < cpos.z || pos.x >= cpos.x + width || pos.y >= cpos.y + height || pos.z >= cpos.z + width)
            {
                continue;
            }
            return Chuncks[i];
        }
        return null;
    }
}

public class Block
{
    public string BlockName;
    public int BlockID;
    public bool Trasnsparent = false;
    public int TextureX;
    public int TextureY;

    public int TextureXSide;
    public int TextureYSide;

    public int TextureXBottom;
    public int TextureYBottom;

    public bool BlockGlow;
    public Color BlockColor = Color.white;

    public Block()
    {
        BlockID = -1;
        Trasnsparent = true;
    }
    public Block(string name, bool transparent, int tX, int tY)
    {
        Trasnsparent = transparent;
        BlockName = name;
        BlockID = BlockList.Blocks.Count;
        TextureX = tX;
        TextureY = tY;
        TextureXSide = tX;
        TextureYSide = tY;
        TextureXBottom = tX;
        TextureYBottom = tY;
    }
    public Block(string name, bool transparent, int tX, int tY, int sX, int sY)
    {
        Trasnsparent = transparent;
        BlockName = name;
        BlockID = BlockList.Blocks.Count;
        TextureX = tX;
        TextureY = tY;
        TextureXSide = sX;
        TextureYSide = sY;
        TextureXBottom = tX;
        TextureYBottom = tY;
    }
    public Block(string name, bool transparent, int tX, int tY, int sX, int sY, int bX, int bY)
    {
        Trasnsparent = transparent;
        BlockName = name;
        BlockID = BlockList.Blocks.Count;
        TextureX = tX;
        TextureY = tY;
        TextureXSide = sX;
        TextureYSide = sY;
        TextureXBottom = bX;
        TextureYBottom = bY;
    }

    public void SetColor(Color color, bool glow)
    {
        BlockGlow = glow;
        BlockColor = color;
    }

    public static Block getBlock(string name)
    {
        foreach (Block b in BlockList.Blocks)
        {
            if (b.BlockName == name)
                return b;
        }
        return new Block();
    }
}