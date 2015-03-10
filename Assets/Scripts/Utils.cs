using UnityEngine;
using System.Collections.Generic;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;
using System.Collections;
using System.Linq;


public static class Utils
{
    public static Color RandomColour()
    {
        float r = Mathf.Abs(UnityRandom.value - 0.3f);
        float g = Mathf.Abs(UnityRandom.value - 0.3f);
        float b = Mathf.Abs(UnityRandom.value - 0.3f);

        return new Color(r, g, b);
    }

    public static Mesh BuildQuad(/*float width, float height, */Vector3 botleft, Vector3 botright, Vector3 topleft, Vector3 topright, Vector3 Origin)
    {
        Mesh mesh = new Mesh();

        // Setup vertices
        Vector3[] newVertices = new Vector3[4];
        /*
        float halfHeight = height * 0.5f;
        float halfWidth = width * 0.5f;
        newVertices[0] = new Vector3(-halfWidth, -halfHeight, 0); //botleft
        newVertices[1] = new Vector3(-halfWidth, halfHeight, 0);//botright
        newVertices[2] = new Vector3(halfWidth, -halfHeight, 0);//topleft
        newVertices[3] = new Vector3(halfWidth, halfHeight, 0);//topright
        */

        newVertices[0] = botleft;
        newVertices[1] = botright;
        newVertices[2] = topleft;
        newVertices[3] = topright;

        // Setup UVs
        Vector2[] newUVs = new Vector2[newVertices.Length];
        newUVs[0] = new Vector2(0, 0);
        newUVs[1] = new Vector2(0, 1);
        newUVs[2] = new Vector2(1, 0);
        newUVs[3] = new Vector2(1, 1);
        
        // Setup triangles
        int[] newTriangles = new int[] { 0, 1, 2, 3, 2, 1 };

        // Setup normals
        Vector3[] newNormals = new Vector3[newVertices.Length];
        for (int i = 0; i < newNormals.Length; i++)
        {
            newNormals[i] = Vector3.forward;
        }

        // Create quad
        mesh.vertices = newVertices;
        mesh.uv = newUVs;
        mesh.triangles = newTriangles;
        mesh.normals = newNormals;

        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] -= Origin;
        }
        mesh.vertices = verts; //Assign the vertex array back to the mesh
        mesh.RecalculateBounds(); //Recalculate bounds of the mesh, for the renderer's sake


        return mesh;
    }

    public static void CenterOnChildren(this Transform aParent)
    {
        var childs = aParent.Cast<Transform>().ToList(); 
        var pos = Vector3.zero;
        foreach (var C in childs)
        {
            pos += C.position;
            C.parent = null;
        }
        pos /= childs.Count;
        aParent.position = pos;
        foreach (var C in childs)
            C.parent = aParent;
    }

    public static void CenterOnObject(this Transform aParent, Transform _child)
    {
        var childs = aParent.Cast<Transform>().ToList();
        foreach (var C in childs)
        {
            C.parent = null;
        }
        aParent.position = _child.position;
        foreach (var C in childs)
            C.parent = aParent;
    }

    public static void Reset(this Transform _temp)
    {
        _temp.transform.localScale = Vector3.one;
        _temp.transform.localRotation = Quaternion.identity;
        _temp.transform.localPosition = Vector3.zero;
    }

    public static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = (float)color.r;
        float green = (float)color.g;
        float blue = (float)color.b;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        return new Color(1, (int)red, (int)green, (int)blue);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Vector3 Round(this Vector3 vector3, int round = 10)
    {
        vector3 = new Vector3(Mathf.Round(vector3.x * round) / round, Mathf.Round(vector3.y * round) / round, Mathf.Round(vector3.z * round) / round);
        return vector3;
    }

    public static Vector3 RoundAbs(this Vector3 vector3, int round = 10)
    {
        float x = Mathf.Abs(Mathf.Round(vector3.x * round) / round);
        if (x == 0)
            x = 0.1f;
        float y = Mathf.Abs(Mathf.Round(vector3.y * round) / round);
        if (y == 0)
            y = 0.1f;

        float z = Mathf.Abs(Mathf.Round(vector3.z * round) / round);
        if (z == 0)
            z = 0.1f;

        vector3 = new Vector3(x, y, z);

        return vector3;
    }

    public static Vector3 RoundRotation(this Vector3 vector3, int round = 15)
    {
        vector3.x = Mathf.Round(vector3.x / round) * round;
        vector3.y = Mathf.Round(vector3.y / round) * round;
        vector3.z = Mathf.Round(vector3.z / round) * round;
        return vector3;
    }

    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex.ToLower();
    }

    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}