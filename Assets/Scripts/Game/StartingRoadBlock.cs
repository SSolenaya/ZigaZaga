using UnityEngine;

namespace Assets.Scripts
{

    public class StartingRoadBlock : RoadBlock
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
