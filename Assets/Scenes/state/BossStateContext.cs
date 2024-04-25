using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class BossStateContext
    {
        public IBossState CurrentState
        {
            get; set;
        }

        private readonly BossController _bossController;

        public BossStateContext(BossController bossController)
        {
            _bossController = bossController;
        }
        public void Transition()
        {
            CurrentState.Handle(_bossController);
        }

        public void Transition(IBossState state)
        {
            CurrentState = state;
            CurrentState.Handle(_bossController);
        }
    }
}
