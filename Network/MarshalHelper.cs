using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace MarshalHelper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DataStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string action;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string name;

        public float x;
        public float y;
    }

    public static class MarshalHelper
    {
        public static T DeserializeMsg<T>(Byte[] data) where T : struct 
        {
            int objsize = Marshal.SizeOf(typeof(T));
            IntPtr buff = Marshal.AllocHGlobal(objsize);
            Marshal.Copy(data, 0, buff, objsize);
            T retStruct = (T)Marshal.PtrToStructure(buff, typeof(T));
            Marshal.FreeHGlobal(buff);
            return retStruct;
        }
        public static Byte[] SerializeMessage<T>(T msg) where T : struct
        {
            int objsize = Marshal.SizeOf(typeof(T));
            Byte[] ret = new Byte[objsize];
            IntPtr buff = Marshal.AllocHGlobal(objsize);
            Marshal.StructureToPtr(msg, buff, true);
            Marshal.Copy(buff, ret, 0, objsize);
            Marshal.FreeHGlobal(buff);
            return ret;
        }
    }
}