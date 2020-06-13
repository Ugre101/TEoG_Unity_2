public class EnemyHolder : CharHolder
{
    public override BasicChar BasicChar { get; protected set; } = new BasicChar();

    public void Setup(AssingEnemy assingEnemy)
    {
        BasicChar = assingEnemy.Setup(BasicChar);
        Bind();
    }
}