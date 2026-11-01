using Content.Shared._Moffstation.IPC.Components;
using Robust.Shared.Containers;

namespace Content.Shared._Moffstation.IPC.Systems;

public sealed partial class IpcRadio : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<IpcRadioComponent, EntInsertedIntoContainerMessage>(OnKeyInserted);

    }


    public static void OnKeyInserted(IpcRadioComponent component, EntInsertedIntoContainerMessage args)
    {

    }
}
