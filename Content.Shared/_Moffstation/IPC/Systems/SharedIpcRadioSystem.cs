using Content.Shared._Moffstation.IPC.Components;
using Content.Shared.Interaction;
using Content.Shared.Radio.Components;
using Content.Shared.Wires;
using Robust.Shared.Containers;

namespace Content.Shared._Moffstation.IPC.Systems;

public sealed partial class IpcRadio : EntitySystem
{
    [Dependency] private SharedContainerSystem _container = null!;

    public override void Initialize()
    {
        SubscribeLocalEvent<IpcRadioComponent, InteractUsingEvent>(OnRadioInteractUsing);
        SubscribeLocalEvent<IpcRadioComponent, ComponentStartup>(OnRadioStartup);


    }

    public bool InsertEncryptionKey(EntityUid target, EntityUid user, EntityUid key)
    {
        if (!TryComp<IpcRadioComponent>(target, out var radio))
            return false;

        if (radio.KeyContainer.ContainedEntities.Count >= radio.KeySlots)
        {
            //_popup.PopupPredicted(Loc.GetString("encryption-key-slots-already-full"), target, user);
            return false;
        }

        if (_container.Insert(key, radio.KeyContainer))
        {
            // _popup.PopupPredicted(Loc.GetString("encryption-key-successfully-installed"), target, user);
            //_audio.PlayPredicted(radio.KeyInsertionSound, target, user);
            return true;
        }

        return false;
    }


    public void OnRadioStartup(Entity<IpcRadioComponent> ent, ref ComponentStartup args)
    {
        ent.Comp.KeyContainer = _container.EnsureContainer<Container>(ent, ent.Comp.EncryptionKeysContainerID);

    }

    private void OnRadioInteractUsing(Entity<IpcRadioComponent> uid, ref InteractUsingEvent args)
    {
        if (args.Handled)
            return;

        if (HasComp<EncryptionKeyComponent>(args.Used))
        {
            InsertEncryptionKey(args.Target, args.User, args.Used);
            args.Handled = true;
        }





    }
}
