# Adventure of Dwarf Mushroom (Nấm Lùn Phiêu Lưu Ký) 🍄

**A classic 2D Platformer featuring a robust Finite State Machine (FSM) character controller and modular gameplay systems.**

<img width="1367" height="770" alt="image" src="https://github.com/user-attachments/assets/5e57a607-1031-4df5-a8fd-78d0bb469d30" />


## 📖 About The Project
This is a solo-developed 2D Action-Platformer inspired by classic titles. The project focuses on "Game Feel," precise controls, and an extensible Enemy AI system capable of handling 20+ different enemy types and traps.

## ⚔️ Technical Features

### 1. Advanced Character Controller (FSM)
- Implemented a **Finite State Machine** to manage player states (Idle, Run, Jump, Double Jump, Attack).
- Seamless integration with **Animator Blend Trees** ensures physics and animations are always perfectly synced.
- Result: Tight, responsive controls with no "floaty" feeling.

### 2. Modular & Decoupled Design
- **Separation of Concerns:** Attack logic is completely decoupled from Movement logic.
- **Component-based Weapon System:** Weapons are independent components, allowing for easy swapping and expansion without modifying the core character code.

### 3. OOP & Enemy AI
- **Inheritance:** Built a solid `BaseEnemy` class that allows for rapid creation of new enemy types (20+ variations).
- **Boss AI:** Features a **Multi-phase Boss** logic to create challenging encounters.

### 4. Performance & Polish
- **Bullet Hell Optimization:** Utilized **Object Pooling** for projectiles and effects to maintain high FPS on mobile devices.
- **Visuals:** Extensive use of **DoTween** for juice and polish.
- **Environment:** Managed using Multi-layer Tilemaps.
- **Persistence:** JSON-based Save/Load system for Checkpoints.

## 🛠 Tech Stack
* **Engine:** Unity 2022.x
* **Core:** 2D Physics, Raycasting.
* **Patterns:** Finite State Machine (FSM), Object Pooling, Component Pattern.
* **Data:** JSON Utility / Newtonsoft.Json.

## 📸 Screenshot
<img width="1420" height="796" alt="image" src="https://github.com/user-attachments/assets/1fae95d2-b1dd-4dcd-b258-99c295a70c8d" />


## 📦 Installation
1.  Clone the repo: `git clone https://github.com/Maz2605/2D_Nam_lun_phieu_luu_ky.git`
2.  Open project in Unity.
3.  Play scene: `Scenes/Loading`.

---
*Developed by [Maz](https://github.com/Maz2605)*
