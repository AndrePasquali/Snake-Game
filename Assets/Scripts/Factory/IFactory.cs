namespace MinoGames.SnakeGame.Factory
{
    public interface IFactory<T>
    {
        T Build();
    }
}