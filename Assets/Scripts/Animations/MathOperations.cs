using UnityEngine;

public static class MathOperations  {


    public static void RotateVector(ref Vector3 vec, float rotateValue)
    {


        Matrix4x4 rotationMatrix = new Matrix4x4();
        float rad = -rotateValue;

        rotationMatrix.SetRow(0, new Vector4(Mathf.Cos(rad), 0, Mathf.Sin(rad), 0));
        rotationMatrix.SetRow(1, new Vector4(0, 0, 0, 0));
        rotationMatrix.SetRow(2, new Vector4(-Mathf.Sin(rad), 0, Mathf.Cos(rad), 0));
        rotationMatrix.SetRow(3, new Vector4(0, 0, 0, 1));
        vec = rotationMatrix.MultiplyVector(vec);
       // Debug.Log("angle :" + rad);

      //  Debug.Log("Rotated Vector :" + vec);
    }

}
