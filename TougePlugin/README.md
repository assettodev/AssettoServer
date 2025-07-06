# Touge Plugin backport for assetto-server 0.0.54

A plugin for Assetto Corsa servers that enables cat-and-mouse-style **touge** racing with an engaging tie-breaker ruleset. Designed for competitive head-to-head driving with a configurable format. Find some live example servers running the plugin [here](https://assetto.scratchedclan.nl/servers).

---

## Features

- **Touge Format**: Race head-to-head in alternating lead/chase roles.
- **Best-of-3 Logic**: Always runs 2 races. If tied (0–0 or 1–1), sudden-death rounds continue until the tie is broken.
- **Fully Configurable**: Tweak rules and behavior through a generated config file.
- **C# + Lua**: Powered by a server-side C# plugin and a client-side Lua UI integration.

---
## Installation

- **Download the plugin**  
    _(Download link will be added here soon)_
- **Extract it**  
    Place the contents into your server's `plugins` directory.
- **Run your server once**  
    This will generate configuration files inside the `cfg` folder.
- **Customize your ruleset**  
    Edit the generated `plugin_touge_cfg.yml` to adjust setting to your liking.  

---

## Configuration


## Configuration
Enable the plugin in `extra_cfg.yml`
```yaml
EnablePlugins:
- TougePlugin
```

### `touge_starting_areas.ini`
Used to setup the starting areas on various maps. You can use [comfy map](https://www.overtake.gg/downloads/comfy-map.52623/) to get the position and heading. The following example sets up two starting areas, one for Gunma and one for Imola. Configurations for different tracks can all be stored in the same file. A download link for a base configuration file that contains various starting positions for popular tracks will be added later. Also, feel free to share your configs in the [Discord](https://discord.gg/z22Pcsy3df).
```
[pk_gunma_cycle_sports_center-gcsc_full_attack_1]
leader_pos = -199.7,467.3,-87.7
leader_heading = -16
chaser_pos = -195.3,467,-83.1
chaser_heading = -17

[imola_1]
leader_pos = -199.7,467.3,-87.7
leader_heading = -16
chaser_pos = -195.3,467,-83.1
chaser_heading = -17
```

Example configuration (add to bottom of `extra_cfg.yml`)
```yaml
---
!TougeConfiguration
# Car performance ratings keyed by car model name. Can range from 1 - 1000.
CarPerformanceRatings:
  ks_mazda_miata: 125
  ks_toyota_ae86: 131
# Maximum elo gain. Must be a positive value.
MaxEloGain: 32
# Number of races for which is player is marked as provisional for the elo system.
ProvisionalRaces: 20
# Maximum elo gain, when player is marked as provisional
MaxEloGainProvisional: 50
# Rolling start enabled.
isRollingStart: false
# Outrun timer in seconds. Chase car has to finish within this amount of time after the lead car crosses the finish line.
outrunTime: 3
# Local database mode enabled. If disabled please provide database connection information.
isDbLocalMode: true
# Connection string to PostgreSQL database. Can be left empty if isDbLocalMode = true.
postgresqlConnectionString:
```

#### Elo Configuration

##### `CarPerformanceRatings`
**Type:** `Dictionary<string, int>`  
**Description:** Specifies performance ratings for different car models.  
**Usage:** Each key represents the car's internal model name (e.g., `ks_mazda_miata`) and the value is a performance score between **1** and **1000**.  
**Purpose:** Used in player elo calculations to improve fairness. Winning in a faster car against a slower car will award less elo gain than beating a fast car using a slower one.  
**Example:**
```yaml
CarPerformanceRatings:
  ks_mazda_miata: 125
  ks_toyota_ae86: 131
```

##### `MaxEloGain`
**Type:** `int`  
**Description:** The maximum amount of elo rating a player can gain (or lose) in a single race.  
**Constraints:** Must be a **positive integer**.  
**Purpose:** Gives control over the volatility of the rating system.

##### `ProvisionalRaces`
**Type:** `int`  
**Description:** The number of initial races a player is considered "provisional" in the elo system.  
**Constraints:** Must be **greater than 0**.  
**Purpose:** Allows for slightly larger elo changes than configured in MaxEloGain while a player's skill is still being established.

##### `MaxEloGainProvisional`
**Type:** `int`  
**Description:** The maximum elo gain/loss while a player is still provisional.  
**Constraints:** Must be **greater than 0**.  
**Purpose:** Allows for a faster elo adjustment during provisional matches compared to regular ones.

#### Race Setup

##### `isRollingStart`
**Type:** `bool`  
**Description:** Enables or disables rolling starts.  
**Usage:**  
- `true`: Cars start moving at the beginning of the race.  
- `false`: Cars are stationary at the start.

##### `outrunTime`
**Type:** `int`  
**Description:** The number of seconds the **chase car** has to cross the finish line after the **lead car** finishes.  
**Constraints:** Must be between **1 and 60 seconds**.  
**Purpose:** Used to determine if the lead car successfully outran the chase car.

#### Database

##### `isDbLocalMode`
**Type:** `bool`  
**Description:** Whether the system should use a local in-memory or file-based database instead of a PostgreSQL server.  
**Usage:**
- `true`: No external DB needed; local data only.
- `false`: Requires valid PostgreSQL connection string.

##### `postgresqlConnectionString`
**Type:** `string?`  
**Description:** Connection string used to connect to a PostgreSQL database.  
**Constraints:**  
- Must be non-empty **only if** `isDbLocalMode` is `false`.  
**Purpose:** Provides data persistence and multi-server support in non-local setups.

**Example:**
```yaml
postgresqlConnectionString: "Host={IP/URL};Port={Port};Username={Username};Password={Password};Database={Database name}"
```

---

## Notes

- Requires Content Manager with recent version of CSP enabled.
- This is a AssettoServer plugin.
	- Tested on [v0.0.55-pre25](https://github.com/compujuckel/AssettoServer/releases/tag/v0.0.55-pre25)
- UI and other plugin features may evolve — stay tuned for updates.

  ## Planned features
  - Configurable finish line, so the plugin is not dependent on the track mod to have timing lines. Allowing users to define the start area and finish line themselves.
  - More rulesets.
  - Improved lap validation.

