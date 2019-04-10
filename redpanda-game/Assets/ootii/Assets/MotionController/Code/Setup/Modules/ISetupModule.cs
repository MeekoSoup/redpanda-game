using com.ootii.Actors.AnimationControllers;

namespace com.ootii.Setup
{
    public interface ISetupModule
    {
#if UNITY_EDITOR
        int Priority { get; }
        string Category { get; }
        bool IsValid { get; }

        void Initialize(bool rUseDefaults);
        void BeginSetup(MotionController rMotionController, bool rIsPlayer);        
#endif
    }
}

