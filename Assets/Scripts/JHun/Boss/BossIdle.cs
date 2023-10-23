namespace Boss
{
    public class BossIdle : IBossAction
    {
        BossController controller;

        public BossIdle(BossController controller)
        {
            this.controller = controller;
        }

        public void Work()
        {
            if (controller.Player != null)
            {
                SetMove();
            }
        }

        private void SetMove()
        {
            controller.State = BossState.Move;
        }
    }

}
