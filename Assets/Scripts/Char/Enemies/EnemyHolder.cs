public class EnemyHolder : CharHolder
{
    public void SetupEnemy(AssingEnemy assingEnemy)
    {
        BasicChar = assingEnemy.Setup(BasicChar);
        Bind();
    }
}