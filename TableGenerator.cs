using g3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ThingOne
{
    public class TableGenerator
    {
        // public double TableHeight = 100;
        // public double TableTopSides = 100;
        // public double TableTopDepth = 10;
        // public double LegDimensions = 10;

        public DMesh3 GenerateTable(double TableHeight, double TableTopSides, double TableTopDepth, double LegDimensions)
        {
            // Vector3d
            // Index3i
            // Box3d, BoxGenerator
            // 

            Box3d TableTop = new Box3d(Vector3d.Zero, new Vector3d(TableTopSides/2, TableTopSides/2, TableTopDepth/2));
            Box3d Leg1 = new Box3d(Vector3d.Zero, new Vector3d(LegDimensions/2, LegDimensions/2, TableHeight/2));
            Box3d Leg2 = new Box3d(Vector3d.Zero, new Vector3d(LegDimensions/2, LegDimensions/2, TableHeight/2));
            Box3d Leg3 = new Box3d(Vector3d.Zero, new Vector3d(LegDimensions/2, LegDimensions/2, TableHeight/2));
            Box3d Leg4 = new Box3d(Vector3d.Zero, new Vector3d(LegDimensions/2, LegDimensions/2, TableHeight/2));


            // Quaterniond rotation = new Quaterniond(Vector3d.AxisZ, 45); 

            DMesh3 TableTopMesh = BoxToMesh(TableTop);
            DMesh3 Leg1Mesh = BoxToMesh(Leg1);
            DMesh3 Leg2Mesh = BoxToMesh(Leg2);
            DMesh3 Leg3Mesh = BoxToMesh(Leg3);
            DMesh3 Leg4Mesh = BoxToMesh(Leg4);


            // MeshTransforms.Rotate(TableTop, Vector3d.Zero, rotation);
            MeshTransforms.Translate(TableTopMesh, new Vector3d(0, 0, TableHeight + TableTopDepth / 2));

            double LegCornerXorY = TableTopSides/2 - LegDimensions / 2; 

            MeshTransforms.Translate(Leg1Mesh, new Vector3d(LegCornerXorY, LegCornerXorY, TableHeight / 2));
            MeshTransforms.Translate(Leg2Mesh, new Vector3d(LegCornerXorY * -1, LegCornerXorY, TableHeight / 2));
            MeshTransforms.Translate(Leg3Mesh, new Vector3d(LegCornerXorY * -1, LegCornerXorY * -1, TableHeight / 2));
            MeshTransforms.Translate(Leg4Mesh, new Vector3d(LegCornerXorY, LegCornerXorY * -1, TableHeight / 2));

            DMesh3 Final = new DMesh3();
            MeshEditor editor = new MeshEditor(Final);
            editor.AppendMesh(TableTopMesh);
            editor.AppendMesh(Leg1Mesh);
            editor.AppendMesh(Leg2Mesh);
            editor.AppendMesh(Leg3Mesh);
            editor.AppendMesh(Leg4Mesh);

            return Final;

        }

        public static void SaveMesh(DMesh3 Mesh, string OutputPath)
        {
            IOWriteResult result = StandardMeshWriter.WriteFile(
                    OutputPath,
                    new List<WriteMesh>() { new WriteMesh(Mesh) },
                    WriteOptions.Defaults);
        }

        public static DMesh3 BoxToMesh(Box3d Box)
        {
            var generator = new TrivialBox3Generator();
            generator.Box = Box;
            generator.Generate();
            return generator.MakeDMesh();
        }

    }
}
