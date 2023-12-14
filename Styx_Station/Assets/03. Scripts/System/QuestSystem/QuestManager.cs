

public class QuestManager 
{
    private QuestBase currentQueset;

    public void ChangeState(QuestBase newQueset)
    {
        if (currentQueset != null)
        {
            currentQueset.Exit();
        }

        currentQueset = newQueset;
        currentQueset.Enter();
    }

    public void Update()
    {
        if (currentQueset != null)
        {
            currentQueset.Update();
        }
    }

    public QuestBase GetCurrentState() { return currentQueset; }
}
