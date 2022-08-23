namespace Filippa;

public abstract class Player
{
    public string Name { get; }

    protected Player(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public abstract IPlayerEngine PlayHand(IReadOnlyList<Card> cards);
}