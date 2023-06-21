using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelMeshCode : MonoBehaviour
{

    void Start(){

        // CREATING AN EXAMPLE MODEL
        GameObject OurObject = new GameObject();
        PixelMesh OurMesh = new PixelMesh(OurObject.transform, new Vector4(12f, 6f, 9f, 1f), "01..........011..........0111........322.........32222.......322.........0111.......011.........01...................................01..........3222222.....32222222222.3222222.....01......................................................................3222........32224434....3222............................................................................................0111........................................................................................................01.........................................................................................................01..........................................................", new Color32[]{ new Color32(137, 0, 0, 255),  new Color32(219, 39, 41, 255),  new Color32(117, 111, 109, 255),  new Color32(0, 69, 129, 255),  new Color32(0, 151, 197, 255) });
        OurObject.transform.parent = this.transform.GetChild(0);
        OurObject.transform.localPosition = Vector3.zero;
        // CREATING AN EXAMPLE MODEL

    }

    // CODE BEHIND THE MODELS
    class PixelMesh {

        // VARIABLES
        Transform Parent;
        float VoxelSize = 1f;
        int mX, mY, mZ = 1;
        string Voxels;
        public Color32[] Colors;

        // FUNCTION THAT PLACES CUBES BASED ON THE STRING
        void Render(){
            // Pass to an array
            for(int Y = 1; Y <= mY; Y++) for(int Z = 1; Z <= mZ; Z++) for(int X = mX; X > 0; X--) if (Voxels.Substring((X+((Z-1)*mX)+((Y-1)*mX*mZ))-1, 1) != "."){
                Vector3 placePos = new Vector3((float)mX/2f - (float)X, (float)mY/-2f + (float)Y, (float)mZ/-2f + (float)Z) * VoxelSize;
                GameObject PutVoxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                PutVoxel.name = Voxels.Substring((X+((Z-1)*mX)+((Y-1)*mX*mZ))-1, 1);
                PutVoxel.transform.SetParent(Parent);
                PutVoxel.transform.localPosition = placePos;
                PutVoxel.transform.localScale = Vector3.one * VoxelSize;
            }
            Draw();
        }

        // FUNCTION THAT PLACES A CUBE
        void Draw(){
            foreach(Transform PaintVoxel in Parent) if (PaintVoxel.name != "."){
                Material VoxelMaterial = PaintVoxel.GetComponent<MeshRenderer>().material;
                Color VoxelColor = Colors[int.Parse(PaintVoxel.name)];
                VoxelMaterial.shader = Shader.Find("VertexLit");
                VoxelMaterial.color = Color.Lerp(VoxelColor, Color.black, 0.5f);
                VoxelMaterial.SetColor("_SpecColor", VoxelColor);
                VoxelMaterial.SetFloat("_Shininess", 0f);
            }
        }

        // CONSTRUCTOR
        public PixelMesh(Transform theParent, Vector4 Size, string setVoxels, Color32[] ColorMap){
            Parent = theParent;
            mX = (int)Size.x; mY = (int)Size.y; mZ = (int)Size.z;
            VoxelSize = Size.w;
            Voxels = setVoxels;
            Colors = ColorMap;
            Render();
        }

        // DECTRUCTOR
        ~PixelMesh(){
            foreach(Transform CleanVoxel in Parent) if (CleanVoxel.name.Length == 1){
                Destroy(CleanVoxel);
            }
        }

    }
    // CODE BEHIND THE MODELS

}
