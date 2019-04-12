public class MouseMoveInput : com.ootii.Input.UnityInputSource
{
    /*
    public override bool IsViewingActivated
    {
        get
        {
            if (!_IsEnabled) { return false; }
            
            bool lValue = false;

            if (_IsXboxControllerEnabled)
            {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                    lValue = (UnityEngine.Input.GetAxis("MXRightStickX") != 0f);
                    if (!lValue) { lValue = (UnityEngine.Input.GetAxis("MXRightStickY") != 0f); }
#else
                lValue = (UnityEngine.Input.GetAxis("WXRightStickX") != 0f);
                if (!lValue) { lValue = (UnityEngine.Input.GetAxis("WXRightStickY") != 0f); }
#endif
            }

            if (!lValue)
            {
                return true;
            }

            return lValue;
            
            return true;
        }
    }
    */
}
