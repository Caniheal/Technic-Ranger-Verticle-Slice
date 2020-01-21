// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class PlayerControls : InputActionAssetReference
{
    public PlayerControls()
    {
    }
    public PlayerControls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // PlayerControl
        m_PlayerControl = asset.GetActionMap("PlayerControl");
        m_PlayerControl_Newaction = m_PlayerControl.GetAction("New action");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_PlayerControl = null;
        m_PlayerControl_Newaction = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // PlayerControl
    private InputActionMap m_PlayerControl;
    private InputAction m_PlayerControl_Newaction;
    public struct PlayerControlActions
    {
        private PlayerControls m_Wrapper;
        public PlayerControlActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction { get { return m_Wrapper.m_PlayerControl_Newaction; } }
        public InputActionMap Get() { return m_Wrapper.m_PlayerControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerControlActions set) { return set.Get(); }
    }
    public PlayerControlActions @PlayerControl
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerControlActions(this);
        }
    }
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get

        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.GetControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    private int m_Newcontrolscheme1SchemeIndex = -1;
    public InputControlScheme Newcontrolscheme1Scheme
    {
        get

        {
            if (m_Newcontrolscheme1SchemeIndex == -1) m_Newcontrolscheme1SchemeIndex = asset.GetControlSchemeIndex("New control scheme1");
            return asset.controlSchemes[m_Newcontrolscheme1SchemeIndex];
        }
    }
}
