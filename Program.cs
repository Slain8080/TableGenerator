using System;
using System.Collections.Generic;
using g3;
using ThingOne;

class Program
{

    static void Main(string[] args)
    {

        double TableHeight = 200;
        double TableTopSides = 180;
        double TableTopDepth = 40;
        double LegDimensions = 20;


        TableGenerator TableGen = new TableGenerator();
        DMesh3 Table = TableGen.GenerateTable(TableHeight, TableTopSides, TableTopDepth, LegDimensions);

        // string inputPath = @"C:\Users\greyc\Downloads\DragonOriginal.stl";
        string outputPath = @"C:\Users\greyc\Downloads\Table.obj";

        SaveMesh(Table, outputPath); 


    }

    public static void ReduceMeshAndSave(string InputPath, int TriangleCount, string OutputPath)
    {
        DMesh3 loadedMesh = null;

        try
        {
            loadedMesh = StandardMeshReader.ReadMesh(InputPath);
            Console.WriteLine($"Mesh read successfully. Vertex count: {loadedMesh.VertexCount}, Triangle count: {loadedMesh.TriangleCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read mesh: {ex.Message}");
        }

        if (loadedMesh != null) 
        {
            Reducer r = new Reducer(loadedMesh);
            r.ReduceToTriangleCount(TriangleCount);

            Console.WriteLine($"Mesh reduced successfully. New vertex count: {loadedMesh.VertexCount}, New triangle count: {loadedMesh.TriangleCount}");

            IOWriteResult result = StandardMeshWriter.WriteFile(
                OutputPath,
                new List<WriteMesh>() { new WriteMesh(loadedMesh) },
                WriteOptions.Defaults);

            Console.WriteLine($"Reduced Mesh successfully written to new file.");
        }
        else
        {
            Console.WriteLine("Function Failed, did not export reduced Mesh");
        }

        


    }

    public static void SaveMesh(DMesh3 Mesh, string OutputPath)
    {
        IOWriteResult result = StandardMeshWriter.WriteFile(
                OutputPath,
                new List<WriteMesh>() { new WriteMesh(Mesh) },
                WriteOptions.Defaults);
    }

}