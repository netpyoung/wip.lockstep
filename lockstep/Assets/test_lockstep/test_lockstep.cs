using UnityEngine;

namespace test_lockstep
{
    public sealed class test_lockstep : MonoBehaviour
    {
        private readonly Lockstep system1 = new Lockstep("1");
        private readonly Lockstep system2 = new Lockstep("2");

        private void Start()
        {
            DummyServer server = new DummyServer();
            this.system1.Start(server);
            this.system2.Start(server);
        }

        private void Update()
        {
            if (!this.system1.IsRunning)
            {
                return;
            }

            // collecting input.
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.system1.Input.PressLeft();
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.system1.Input.PressRight();
            }


            if (Input.GetKey(KeyCode.A))
            {
                this.system2.Input.PressLeft();
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.system2.Input.PressRight();
            }


            // ticking system.
            float dt = Time.deltaTime;
            int x = (int) (dt * 1000);
            this.system1.Tick(x);
            this.system2.Tick(x);
        }
    }
}