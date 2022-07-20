# Unity Modes

Easily switch between or create new Unity editor modes — it uses the same system that Unity's builtin Safe Mode does or the Standalone Profiler.

### How to use

- open ``Window/Needle/Unity Modes Window`` to easily switch between currently known modes (or shortcut ``Ctrl+F12``)
- create new modes by creating a ``.mode`` file with your configuration (see builtin or modes in this package as reference)
- startup Unity with a mode by passing the ``-mode`` command line argument with a name (must be the ``label`` field of the mode. e.g. ``-mode "Needle Console And Scene View"``). Buitin modes can be found in ``Editor\Data\Resources\default.mode``