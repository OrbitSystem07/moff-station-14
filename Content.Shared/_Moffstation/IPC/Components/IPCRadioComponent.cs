using Content.Shared.Radio.Components;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;

namespace Content.Shared._Moffstation.IPC.Components;

[RegisterComponent, NetworkedComponent]
public sealed partial class IpcRadioComponent : Component
{
    [DataField]
    public int KeySlots = 4;

    [ViewVariables]
    public Container KeyContainer = null!;



    [DataField]
    public string EncryptionKeysContainerID = "key_slots";


    [ViewVariables(VVAccess.ReadWrite)]
    public IntrinsicRadioTransmitterComponent RadioTransmitter = null!;

    [ViewVariables(VVAccess.ReadWrite)]
    public ActiveRadioComponent ActiveRadio = null!;


}
