using UnityEngine;

namespace Assets.Scripts {

    public class StartingPlatformBlock : RoadBlock
    {
        Vector3 myEndPos = new Vector3(0, 0, 1.5f);             //  temp

        public override void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public override void SetupAsDefault()
        {
            _botTrigger.ChangeState(false);
            SetPhysicState(BlockStates.steady);
        }

        public override Vector3 GetTurningPoint()
        {
            return myEndPos;
        }

    }
}
