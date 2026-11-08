using Content.Shared._Moffstation.IPC.Components;
using Robust.Shared.Containers;
using Content.Shared.Radio.Components;

namespace Content.Server._Moffstation.Species.Ipc;

public sealed class IpcRadioSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<IpcRadioComponent, EntInsertedIntoContainerMessage>(OnKeyInserted);

    }


    public void OnKeyInserted(Entity<IpcRadioComponent> ent, ref EntInsertedIntoContainerMessage args)
    {
        UpdateChannels(ent);


    }

    public void UpdateChannels(Entity<IpcRadioComponent> ent)
    {
        ent.Comp.ActiveRadio.Channels.Clear();
        ent.Comp.RadioTransmitter.Channels.Clear();

        foreach (var key in ent.Comp.KeyContainer.ContainedEntities)
        {
            if (TryComp<EncryptionKeyComponent>(key, out var keyComponent))
            {
                ent.Comp.ActiveRadio.Channels.UnionWith(keyComponent.Channels);
                //Theres probably a better way to not give them common, but i dont know it
                if ((keyComponent.Channels = ["Common"]) != null)
                {
                    ent.Comp.RadioTransmitter.Channels.UnionWith(keyComponent.Channels);
                }


            }


        }
    }
}
