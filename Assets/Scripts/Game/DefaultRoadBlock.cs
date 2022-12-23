namespace Assets.Scripts {

    public class DefaultRoadBlock : RoadBlock
    {
        public override void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public override void SetupAsDefault()
        {
            _botTrigger.ChangeState(false);
            SetPhysicState(BlockStates.steady);
        }
    }
}
