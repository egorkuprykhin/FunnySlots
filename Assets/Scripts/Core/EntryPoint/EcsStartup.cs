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

        private CoreFactory _coreFactory;
        private FieldPositionsService _fieldPositionsService;
        private CombinationService _combinationsService;
        private CardsInitializeDataService _cardInitializeDataService;

        [Inject]
        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        void Start ()
        {
            SharedData sharedData = new SharedData();

            _coreFactory = new CoreFactory(Configuration);
            _combinationsService = new CombinationService();
            _fieldPositionsService = new FieldPositionsService(Configuration, sharedData);
            _cardInitializeDataService = new CardsInitializeDataService(Configuration.CardsData);

            _world = new EcsWorld ();
            
            EcsExtensionsService.World = _world;
            
            _systems = new EcsSystems (_world, sharedData);
            _systems
                .Add (new InitWorldSystem ())
                .Add (new InitCameraSystem ())
                .Add (new InitFieldMaskSystem ())
                .Add (new SharedDataUpdateSystem ())
                .Add (new SoundSystem ())
                
                .Add (new CreateCardSystem ())
                .Add (new MoveCardSystem ())
                
                .Add (new WatchHighestCardSystem ())
                
                .Add (new HudButtonsSystem ())
                
                .Add(new RollingStateWatcherSystem ())
                .Add(new MetaMediatorSystem ())
                
                .Add (new StartRollSystem ())
                .Add (new StopCardInTargetPositionSystem ())
                .Add (new CreateCardRequestSystem ())
                
                .Add( new PrepareCombinationSystem ())
                .Add( new SelectWinCombinationSystem ())
                .Add( new WinCombinationSystem ())
                
                .Add (new UpdateViewPositionSystem ())
                .Add (new ScoresSystem ())
                
                .Add (new DestroyCardRequestSystem ())
                .Add (new StopRowInTimeRequestSystem ())
                
                .Add(new DestroyCardSystem ())
                
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                
                .Inject(Configuration, SceneData)
                
                .Inject(_coreFactory)
                .Inject(_cardInitializeDataService)
                .Inject(_fieldPositionsService)
                .Inject(_combinationsService)
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