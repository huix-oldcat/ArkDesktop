# ArkDesktop

CI: [![Build status](https://ci.appveyor.com/api/projects/status/f4oklwdady0figt2?svg=true)](https://ci.appveyor.com/project/huix-oldcat/arkdesktop)

方便地写一个桌面控件/悬浮窗/...



## 现在它能做什么?

1. 使用`ArkDesktopStaticPic`插件播放静帧(支持透明背景),并可以稳定置顶(可覆盖任务栏)和不稳定置于桌面(视操作系统)
2. 动态载入插件
3. 多配置管理,但现在只能单进程运行单实例,所以如果要同时开多个实例就要开多个软件,并且考虑到配置冲突,必须让它们的运行目录相互隔离

## 许可证

项目里面的所有源代码均遵循 The Mozilla Public License 2.0 许可证

## 更新计划

- [ ] 保存窗口是否置顶等
- [ ] 修复"置于桌面"的功能
- [ ] StaticPic增加自定义逻辑,目前准备包含点击逻辑
- [ ] 置顶窗口把鼠标移上去应该透明并且可以穿透点击
- [ ] 集成Chormium内核(ChromiumFX)
- [ ] 增强权限管理