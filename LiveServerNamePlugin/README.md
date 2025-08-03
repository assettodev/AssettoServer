# LiveServerNamePlugin
An [AssettoServer](https://github.com/compujuckel/AssettoServer) plugin that will automatically change your server name during runtime.

### Configuration
Enable the plugin in your `extra_cfg.yml`
```YAML
EnablePlugins:
- LiveServerNamePlugin
```

Add the plugin configuration to the bottom of your `extra_cfg.yml`
```YAML
---
!LiveServerNameConfiguration
#This value is in seconds, only ranges from 1-7200
UpdateInterval: 6
#By default the plugin will go through each entry in order starting with the first, setting this to 'true' will instead choose a random entry
Randomize: false
#The plugin will only read the first 20 entries
ListOfNames:
  - "Assetto Corsa Server | New server come cruise!"
  - "Assetto Corsa Server | Drive responsibly!"
  - "Assetto Corsa Server | We have new cars!"
  - "Assetto Corsa Server | discord.gg/invite"
```
