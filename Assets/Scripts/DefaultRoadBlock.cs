namespace Assets.Scripts {

    public class DefaultRoadBlock : RoadBlock
    {
        public override void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}
