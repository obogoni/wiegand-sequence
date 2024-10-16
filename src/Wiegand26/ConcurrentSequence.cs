using System;
using CSharpFunctionalExtensions;

namespace Wiegand26;

public class ConcurrentSequence
{
    private object syncRoot = new object();
    private Sequence sequence;

    public ConcurrentSequence()
    {
        this.sequence = Sequence.NewSequence();
    }

    public ConcurrentSequence(Sequence sequence)
    {
        this.sequence = sequence;
    }

    public Result<Card> Next()
    {
        lock (syncRoot)
        {
            return sequence.Next();
        }
    }
}
