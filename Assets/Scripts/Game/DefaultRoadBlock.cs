using UnityEngine;

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

        public override Vector3 GetTurningPoint()
        {
            Vector3 myEndPos = new Vector3(0, 0, 1.5f);
            return myEndPos;
        }

    }
}
