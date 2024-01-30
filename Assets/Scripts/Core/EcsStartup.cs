using FSM;
using FunnySlots.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using VContainer;

namespace FunnySlots {
    public sealed class EcsStartup : MonoBehaviour
    {
        private StateMachine _stateMachine;
        
        EcsWorld _world;
        IEcsSystems _systems;

        public Configuration Configuration;
        public SceneData SceneData;
        public EcsUguiEmitter UguiEmitter;

        private CardsSpriteSelectorService _cardSpriteSelectionService;
        private FieldPositionsService _fieldPositionService;

        [Inject]
        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        void Start ()
        {
            SharedData sharedData = new SharedData();
            
            _cardSpriteSelectionService = new CardsSpriteSelectorService(Configuration.CardsData);
            _fieldPositionService = new FieldPositionsService(Configuration, sharedData);
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world, sharedData);
            _systems
                .Add (new InitWorldSystem ())
                .Add (new InitCameraSystem ())
                .Add (new InitFieldMaskSystem ())
                .Add (new SharedDataUpdater ())
                .Add (new SoundSystem ())
                
                .Add (new CardLifecycleSystem ())
                .Add (new CreateNewCardsSystem ())
                .Add (new CardsMovingSystem ())
                
                .Add (new WatchHighestCardSystem ())
                
                .Add (new HudPlayButtonSystem ())
                .Add (new HudBackButtonSystem ())
                .Add (new StartRollSystem ())
                .Add (new StopRowInTimeSystem ())
                .Add (new StopCardInTargetPosition ())
                
                .Add( new CheckWinSystem ())
                .Add( new WinRollSystem ())
                
                .Add (new UpdateViewPositionSystem ())
                .Add (new ScoresSystem ())
                
                .Add (new DestroyOldCardsSystem ())
                
                
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                
                .Inject(Configuration, SceneData)
                
                .Inject(_cardSpriteSelectionService)
                .Inject(_fieldPositionService)
                .Inject(_stateMachine)
                
                .InjectUgui(UguiEmitter)
                
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}