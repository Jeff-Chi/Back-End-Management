namespace Management.Infrastructure
{
    public static class KeyGenerator
    {
        public static byte[] Random(int length)
        {
            Random rand = new Random(Environment.TickCount);
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = (byte)rand.Next(0, 256);
            }
            return data;
        }
    }
}
