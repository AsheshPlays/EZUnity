/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-11-07 14:06:53
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;
using UnityEngine.Playables;

namespace EZhex1991.EZUnity.Playables
{
    public class EZTransformLookAtMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Transform binding = playerData as Transform;
            if (binding == null) return;

            int inputCount = playable.GetInputCount();
            if (inputCount == 0) return;

            float totalWeight = 0;
            Vector3 lookAtDirection = new Vector3();
            for (int i = 0; i < inputCount; i++)
            {
                var inputPlayable = (ScriptPlayable<EZTransformConstraintBehaviour>)playable.GetInput(i);
                var inputBehaviour = inputPlayable.GetBehaviour();
                if (inputBehaviour.target == null || inputBehaviour.target.position == binding.position) continue;

                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight == 0) continue;

                totalWeight += inputWeight;

                lookAtDirection += (inputBehaviour.target.position - binding.position).normalized * inputWeight;
            }
            if (totalWeight < 1e-5) return;
            binding.rotation = Quaternion.LookRotation(lookAtDirection, binding.up);
        }
    }
}
