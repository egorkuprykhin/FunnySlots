using FSM;
using FunnySlots.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using VContainer;

namespace FunnySlots 
{
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
            _fieldPositionsService = new FieldPositionsService(Configuration);
            _cardInitializeDataService = new CardsInitializeDataService(Configuration.CardsData);

            _world = new EcsWorld ();
            
            EcsEntityExtensions.World = _world;

            _systems = new EcsSystems (_world, sharedData);
            _systems
                .Add <InitWorldSystem>()
                .Add <InitCameraSystem>()
                .Add <InitFieldMaskSystem>()
                .Add <InitScoresSystem>()
                .Add <SoundSystem>()
                
                .Add <CreateCardSystem>()
                .Add <MoveCardSystem>()
                .Add <WatchHighestCardSystem>()
                
                .Add <HudButtonsSystem>()
                .Add <RollingStateWatcherSystem>()
                .Add <MetaMediatorSystem>()
                .Add <CleanCombinationsSystem>()
                
                .Add <StartRollSystem>()
                .Add <StopCardInTargetPositionSystem>()
                .Add <CreateCardRequestSystem>()
                
                .Add <PrepareCombinationSystem>()
                .Add <SelectWinCombinationSystem>()
                .Add <WinCombinationSystem>()
                
                .Add <UpdateViewPositionSystem>()
                .Add <ScoresSystem>()
                
                .Add <DestroyCardRequestSystem>()
                .Add <StopRowInTimeRequestSystem>()
                
                .Add<DestroyCardSystem>()
                
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                
                .Inject(_stateMachine)
                
                .Inject(SceneData)
                .Inject(Configuration)
                
                .Inject(_coreFactory)
                .Inject(_cardInitializeDataService)
                .Inject(_fieldPositionsService)
                .Inject(_combinationsService)
                
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