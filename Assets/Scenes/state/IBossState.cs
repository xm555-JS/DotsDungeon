namespace Chapter.State
{
    public interface IBossState
    {
        void Handle(BossController _bossController);
    }
}