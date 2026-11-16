using Content.Shared._Moffstation.IPC.Components;
using Robust.Shared.Containers;
using Content.Shared.Radio.Components;
using Content.Shared.Radio.EntitySystems;

namespace Content.Server._Moffstation.Species.Ipc;

public sealed partial class IpcRadioSystem : EntitySystem
{

    [Dependency] private SharedRadioSystem _radio = null!;
    public override void Initialize()
    {
        SubscribeLocalEvent<IpcRadioComponent, EntInsertedIntoContainerMessage>(OnKeyInserted);

    }


    public void OnKeyInserted(Entity<IpcRadioComponent> ent, ref EntInsertedIntoContainerMessage args)
    {
        if (!TryComp<IntrinsicRadioTransmitterComponent>(ent.Owner, out var transmitter))
        {
            return;
        }

        ent.Comp.ActiveRadioChannels.Clear();
        ent.Comp.TransmitterRadioChannels.Clear();
        foreach (var key in ent.Comp.KeyContainer.ContainedEntities)
        {
            if (TryComp<EncryptionKeyComponent>(key, out var keyComponent))
            {
                //TODO: Fix it so that common isnt added
                ent.Comp.ActiveRadioChannels.UnionWith(keyComponent.Channels);
                ent.Comp.TransmitterRadioChannels.UnionWith(keyComponent.Channels);

            }
        }

        UpdateChannels(ent);
    }

    private void UpdateChannels(Entity<IpcRadioComponent> ent)
    {


        if (TryComp(ent, out IntrinsicRadioTransmitterComponent? transmitter))
            _radio.SetIntrinsicTransmitterChannels((ent, transmitter), ent.Comp.TransmitterRadioChannels);

        if(TryComp(ent, out ActiveRadioComponent? activeRadio))
            activeRadio.Channels = ent.Comp.ActiveRadioChannels;
    }
}
