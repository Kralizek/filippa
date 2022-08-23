using Filippa;

var deck = new Deck();

var match = new FilippaMatch(deck) { PassCards = false };

match.AddPlayer(new FakePlayer("Player 1"));
match.AddPlayer(new FakePlayer("Player 2"));
match.AddPlayer(new FakePlayer("Player 3"));
match.AddPlayer(new FakePlayer("Player 4"));

var results = match.Play(winningScore: 200);
