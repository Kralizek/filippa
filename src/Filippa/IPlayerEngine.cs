namespace Filippa;

public interface IPlayerEngine
{
    Card[] PassCards();

    void ReceiveCards(Card[] cards);

    void PlayTrick(Trick trick);
}