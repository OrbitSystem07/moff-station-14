using Content.Shared._Moffstation.IPC.Components;
using Robust.Shared.Containers;

namespace Content.Server._Moffstation.Species.Ipc;

public sealed class IpcRadioSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<IpcRadioComponent, EntInsertedIntoContainerMessage>(OnKeyInserted);
    }


    public static void OnKeyInserted(IpcRadioComponent component, EntInsertedIntoContainerMessage args)
    {

    }
}
