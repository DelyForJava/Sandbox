using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true)]
public class OdaoFieldAttribute : Attribute
{
    public OdaoFieldAttribute(int size)
    {
        m_iSize = size;
    }

    public int SizeConst
    {
        get { return m_iSize; }
        set { m_iSize = value; }
    }

    private int m_iSize;
}
