using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class BlockData
{
	public Block.BlockType[,,] matrix;

	public BlockData() { }

	public BlockData(Block[,,] b)
	{
		matrix = new Block.BlockType[World.chunkSize, World.chunkSize, World.chunkSize];
		for (int z = 0; z < World.chunkSize; z++)
			for (int y = 0; y < World.chunkSize; y++)
				for (int x = 0; x < World.chunkSize; x++)
				{
					matrix[x, y, z] = b[x, y, z].bType;
				}
	}
}

public class Chunk
{

	public Material cubeMaterial;
	public Material fluidMaterial;
	public Block[,,] chunkData;
	public GameObject chunk;
	public GameObject fluid;
	public enum ChunkStatus { DRAW, DONE, KEEP };
	public ChunkStatus status;
	public ChunkMB mb;
	BlockData bd;
	public bool changed = false;

	string BuildChunkFileName(Vector3 v)
	{
		return Application.persistentDataPath + "/savedata/Chunk_" +
								(int)v.x + "_" +
									(int)v.y + "_" +
										(int)v.z +
										"_" + World.chunkSize +
										"_" + World.radius +
										".dat";
	}

	bool Load() //read data from file
	{
		/*string chunkFile = BuildChunkFileName(chunk.transform.position);
		if(File.Exists(chunkFile))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(chunkFile, FileMode.Open);
			bd = new BlockData();
			bd = (BlockData) bf.Deserialize(file);
			file.Close();
			//Debug.Log("Loading chunk from file: " + chunkFile);
			return true;
		}*/
		return false;
	}

	public void Save() //write data to file
	{
		/*string chunkFile = BuildChunkFileName(chunk.transform.position);
		
		if(!File.Exists(chunkFile))
		{
			Directory.CreateDirectory(Path.GetDirectoryName(chunkFile));
		}
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(chunkFile, FileMode.OpenOrCreate);
		bd = new BlockData(chunkData);
		bf.Serialize(file, bd);
		file.Close();*/
	}
	
	//This does nothing at startup
	void Start()
    {
		UpdateChunk();
    }

	public void UpdateChunk()
	{
		for (int z = 0; z < World.chunkSize; z++)
			for (int y = 0; y < World.chunkSize; y++)
				for (int x = 0; x < World.chunkSize; x++)
				{
					if (chunkData[x, y, z].bType == Block.BlockType.SAND)
					{
						mb.StartCoroutine(mb.Drop(chunkData[x, y, z],
										Block.BlockType.SAND,
										20));
					}
				}

	}

	void BuildChunk()
	{
		bool dataFromFile = false;
		dataFromFile = Load();

		chunkData = new Block[World.chunkSize, World.chunkSize, World.chunkSize];
		for (int z = 0; z < World.chunkSize; z++)
		{
			for (int y = 0; y < World.chunkSize; y++)
			{
				for (int x = 0; x < World.chunkSize; x++)
				{
					Vector3 pos = new Vector3(x, y, z);
					int worldX = (int)(x + chunk.transform.position.x);
					int worldY = (int)(y + chunk.transform.position.y);
					int worldZ = (int)(z + chunk.transform.position.z);

					if (dataFromFile)
					{
						chunkData[x, y, z] = new Block(bd.matrix[x, y, z], pos,
										chunk.gameObject, this);
						continue;
					}


					int surfaceHeight = Utils.GenerateHeight(worldX, worldZ);

					//This first if statement allows for air blocks to be embedded in the base to create caves.
					if //(Utils.fBM3D(worldX, worldY, worldZ, 0.1f, 3) < 0.42f)
					   //chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos,
					   //			chunk.gameObject, this);
					   //else if 
					   (worldY == 0)
					{ 
						chunkData[x, y, z] = new Block(Block.BlockType.DIAMOND, pos,
									  chunk.gameObject, this);

					//Blocks that are already created in the world
					Vector3 pos1 = new Vector3(7, 1, 11);
					chunkData[7, 1, 11] = new Block(Block.BlockType.BLUE, pos1,
													chunk.gameObject, this);
					Vector3 pos2 = new Vector3(7, 2, 11);
					chunkData[7, 2, 11] = new Block(Block.BlockType.BLUE, pos2,
													chunk.gameObject, this);
					Vector3 pos3 = new Vector3(9, 1, 11);
					chunkData[9, 1, 11] = new Block(Block.BlockType.BLUE, pos3,
													chunk.gameObject, this);
					Vector3 pos4 = new Vector3(9, 2, 11);
					chunkData[9, 2, 11] = new Block(Block.BlockType.BLUE, pos4,
													chunk.gameObject, this);

					Vector3 pos5 = new Vector3(7, 1, 8);
					chunkData[7, 1, 8] = new Block(Block.BlockType.BLUE, pos5,
													chunk.gameObject, this);
					Vector3 pos6 = new Vector3(7, 2, 8);
					chunkData[7, 2, 8] = new Block(Block.BlockType.BLUE, pos6,
													chunk.gameObject, this);
					Vector3 pos7 = new Vector3(9, 1, 8);
					chunkData[9, 1, 8] = new Block(Block.BlockType.BLUE, pos7,
													chunk.gameObject, this);
					Vector3 pos8 = new Vector3(9, 2, 8);
					chunkData[9, 2, 8] = new Block(Block.BlockType.BLUE, pos8,
													chunk.gameObject, this);

					Vector3 pos9 = new Vector3(7, 1, 4);
					chunkData[7, 1, 4] = new Block(Block.BlockType.STONE, pos9,
														chunk.gameObject, this);
					Vector3 pos10 = new Vector3(7, 1, 3);
					chunkData[7, 1, 3] = new Block(Block.BlockType.STONE, pos10,
														chunk.gameObject, this);
					Vector3 pos11 = new Vector3(7, 1, 2);
					chunkData[7, 1, 2] = new Block(Block.BlockType.STONE, pos11,
														chunk.gameObject, this);
					Vector3 pos12 = new Vector3(7, 1, 1);
					chunkData[7, 1, 1] = new Block(Block.BlockType.STONE, pos12,
														chunk.gameObject, this);
					Vector3 pos13 = new Vector3(7, 1, 0);
					chunkData[7, 1, 0] = new Block(Block.BlockType.STONE, pos13,
														chunk.gameObject, this);

					Vector3 pos14 = new Vector3(8, 1, 4);
					chunkData[8, 1, 4] = new Block(Block.BlockType.STONE, pos14,
														chunk.gameObject, this);
					Vector3 pos15 = new Vector3(8, 1, 3);
					chunkData[8, 1, 3] = new Block(Block.BlockType.STONE, pos15,
														chunk.gameObject, this);
					Vector3 pos16 = new Vector3(8, 1, 2);
					chunkData[8, 1, 2] = new Block(Block.BlockType.STONE, pos16,
														chunk.gameObject, this);
					Vector3 pos17 = new Vector3(8, 1, 1);
					chunkData[8, 1, 1] = new Block(Block.BlockType.STONE, pos17,
														chunk.gameObject, this);
					Vector3 pos18 = new Vector3(8, 1, 0);
					chunkData[8, 1, 0] = new Block(Block.BlockType.STONE, pos18,
														chunk.gameObject, this);

					Vector3 pos19 = new Vector3(7, 2, 4);
					chunkData[7, 2, 4] = new Block(Block.BlockType.STONE, pos19,
														chunk.gameObject, this);
					Vector3 pos20 = new Vector3(7, 2, 3);
					chunkData[7, 2, 3] = new Block(Block.BlockType.STONE, pos20,
														chunk.gameObject, this);
					Vector3 pos21 = new Vector3(7, 2, 2);
					chunkData[7, 2, 2] = new Block(Block.BlockType.STONE, pos21,
														chunk.gameObject, this);
					Vector3 pos22 = new Vector3(7, 2, 1);
					chunkData[7, 2, 1] = new Block(Block.BlockType.STONE, pos22,
														chunk.gameObject, this);
					Vector3 pos23 = new Vector3(7, 2, 0);
					chunkData[7, 2, 0] = new Block(Block.BlockType.STONE, pos23,
														chunk.gameObject, this);

					Vector3 pos24 = new Vector3(8, 2, 4); //8 2 5
					chunkData[8, 2, 4] = new Block(Block.BlockType.SAND, pos24,
														chunk.gameObject, this);
					Vector3 pos25 = new Vector3(8, 2, 3);
					chunkData[8, 2, 3] = new Block(Block.BlockType.SAND, pos25,
														chunk.gameObject, this);
					Vector3 pos26 = new Vector3(8, 2, 2);
					chunkData[8, 2, 2] = new Block(Block.BlockType.SAND, pos26,
														chunk.gameObject, this);
					Vector3 pos27 = new Vector3(8, 2, 1);
					chunkData[8, 2, 1] = new Block(Block.BlockType.SAND, pos27,
														chunk.gameObject, this);
					Vector3 pos28 = new Vector3(8, 2, 0);
					chunkData[8, 2, 0] = new Block(Block.BlockType.SAND, pos28,
														chunk.gameObject, this);

					}
					else if
				 (worldY <= Utils.GenerateStoneHeight(worldX, worldZ))
					{
						if (Utils.fBM3D(worldX, worldY, worldZ, 0.01f, 2) < 0.4f && worldY < 40)
							chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, //Originally DIAMOND
										chunk.gameObject, this);
						else if (Utils.fBM3D(worldX, worldY, worldZ, 0.03f, 3) < 0.41f && worldY < 20)
							chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, //Originally REDSTONE
										chunk.gameObject, this);
						else
							chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, //Originally STONE
										chunk.gameObject, this);
					}
					else if (worldY == surfaceHeight)
					{
						chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, //Originally GRASS
										chunk.gameObject, this);
					}
					else if (worldY < surfaceHeight)
						chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos, //Originally DIRT
										chunk.gameObject, this);
					else
					{
						chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos,
										chunk.gameObject, this);
					}

					status = ChunkStatus.DRAW;

				}
			}
		}


	}

	public void Redraw()
	{
		GameObject.DestroyImmediate(chunk.GetComponent<MeshFilter>());
		GameObject.DestroyImmediate(chunk.GetComponent<MeshRenderer>());
		GameObject.DestroyImmediate(chunk.GetComponent<Collider>());
		DrawChunk();
	}

	public void DrawChunk()
	{
		for (int z = 0; z < World.chunkSize; z++)
			for (int y = 0; y < World.chunkSize; y++)
				for (int x = 0; x < World.chunkSize; x++)
				{
					chunkData[x, y, z].Draw();
				}
		CombineQuads();
		MeshCollider collider = chunk.gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		collider.sharedMesh = chunk.transform.GetComponent<MeshFilter>().mesh;
		status = ChunkStatus.DONE;
	}

	public Chunk() { }
	// Use this for initialization
	public Chunk(Vector3 position, Material c)
	{

		chunk = new GameObject(World.BuildChunkName(position));
		chunk.transform.position = position;
		mb = chunk.AddComponent<ChunkMB>();
		mb.SetOwner(this);
		cubeMaterial = c;
		BuildChunk();
	}

	public void CombineQuads()
	{
		//1. Combine all children meshes
		MeshFilter[] meshFilters = chunk.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			i++;
		}

		//2. Create a new mesh on the parent object
		MeshFilter mf = (MeshFilter)chunk.gameObject.AddComponent(typeof(MeshFilter));
		mf.mesh = new Mesh();

		//3. Add combined meshes on children as the parent's mesh
		mf.mesh.CombineMeshes(combine);

		//4. Create a renderer for the parent
		MeshRenderer renderer = chunk.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material = cubeMaterial;

		//5. Delete all uncombined children
		foreach (Transform quad in chunk.transform)
		{
			GameObject.Destroy(quad.gameObject);
		}

	}

}
