using Content.Shared.Radio;
using Content.Shared.Radio.Components;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

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

    [DataField]
    public HashSet<ProtoId<RadioChannelPrototype>> ActiveRadioChannels = new();

    [DataField]
    public HashSet<ProtoId<RadioChannelPrototype>> TransmitterRadioChannels = new();

}
