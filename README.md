# PoolDomain-Unity3d
![GitHub](https://img.shields.io/github/license/svermeulen/Extenject)

# Table Of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<summory>

  - [Introduction](#introduction)
  - [Features](#features)
  - [Installation](#installation)
  - [Startup](#startup)
  - [How To Use](#how-to-use)
</summory>

# Introduction
This library provides the object pooling features and some more related feature to boost development. for example, it’s triggered some callback when object pull out from pool and back to pool or by using by service provider abstraction could be get services on startup and by some configurating could provide specific method argument injection. fully Implemented by C# language although fully depend on Unity Engine libraries it can just be used in that environment.

Support platforms: 
* PC/Mac/Linux
* iOS
* Android
* WebGL
* UWP App


* This library is design to be dependecy injection friendly, the recommended DI library is the [Dependency Injection](https://github.com/Game-Warriors/DependencyInjection-Unity3d) to be used.

```
```
This library used in the following games and apps:
</br>
[Street Cafe](https://play.google.com/store/apps/details?id=com.aredstudio.streetcafe.food.cooking.tycoon.restaurant.idle.game.simulation)
</br>
[Idle Family Adventure](https://play.google.com/store/apps/details?id=com.aredstudio.idle.merge.farm.game.idlefarmadventure)
</br>
[CLC BA](https://play.google.com/store/apps/details?id=com.palsmobile.clc)

# Features
* Setup pool system by editor.
* Async loading in startup.
* Initialization, pop and push callback on object
* Specific and configurable Method dependency injection

# Installation
This library can be added by unity package manager form git repository or could be downloaded.
for more information about how to install a package by unity package manager, please read the manual in this link:
[Install a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

# Startup
After adding package to using the library features, following objects have to Initialize.

PoolSystem: This class encapsulate all system logics and feature like loading pool data and setup objects or adding and getting objects in pool.

* BehaviorInitializer:
The system has specific container to handle “MonoBehaviour“ driven classes. the system could initialize the unity script classes some better way and for this purpose there's are some initializer classes that everyone is use for specific initialization way.

* InjectInitializer: This class apply method argument injection on unity “MonoBehaviour“ driven classes script on specific method by the name ”Initialize”. all the class arguments which registered in service provider will inject.

* LocatorInitializer: This class apply method call by the polymorphism way, call the "Initialize" method and pass the service provider on the classes which implement "IInitializable" interface.

* CompondInitializer: This class apply both method argument injection and method call by the polymorphism. Injecting on unity “MonoBehaviour” driven classes script on specific method by the name “Initialize”. Call the "Initialize" method and pass the service provider on the classes which implement “IInitializable” interface.

* DefaultPoolResourceLoader: This class is default resource loader for the “PoolSystem“ which is using the unity resources class and loading data async. after triggering “onLoad” callback the asset is unloaded.

Here is the sample code of initializing “PoolSystem“ if “IPoolResources“ not passed the system automatedly set the “DefaultResourceLoader“ as resource loader.

```csharp
private async void Awake()
{
    IServiceProvider serviceProvider = new ServiceProvider();
    CompondInitializer compondInitializer = new CompondInitializer(serviceProvider);
    PoolSystem poolSystem = new PoolSystem(compondInitializer, null);
    await poolSystem.WaitForLoading();
    // use the pool system
}
```
Overall, Pool system need two abstractions for initialization.

1.  __IBehaviorInitializer:__ The base abstraction of “MonoBehaviour” class scripts initializer which use for the initialization pipeline by pool system.

2.  __IPoolResources:__ The abstraction which using by pool system to load require data.

If the dependency injection library is used, the initialization process could be like following sample.
```csharp
private async void Awake()
{
    serviceCollection = new ServiceCollection(INIT_METHOD_NAME);
    serviceCollection.AddSingleton<IPool, PoolSystem>(input => input.WaitForLoading());
    serviceCollection.AddSingleton<IPoolResources, DefaultPoolResourceLoader>();
    serviceCollection.AddSingleton<IBehaviorInitializer<string>, CompondInitializer>();
    await serviceCollection.Build(StartupDone);
}
```
In the final setup state is using the object pool editor, the prefabs which should be add in object pool system editor to add in three pool containers.

1.  __GameObject Pool:__ The pool container which is specify for the game objects and directly pool by unity3d “GameObject” reference.

2.  __Component Pool:__ The pool container which is specify for the component derived classes and directly pool by unity3d component class reference.

2.  __Behavior Pool:__ The pool container which is specify for the “MonoBehaviour” derived classes and directly pool by script class reference.

The editor could be open by selecting Tools->Pool Configuration in top navigation bar. each pool item has some property inputs.

* Key: Key of the object will be name of its prefab. the key is very important because it’s use later to fetch object from pool container.

* Pool Count: The pool count value is initial count of instantiating object before startup. when the size of the pool counter is increased after more than the number of requests, the number increases and will resize. the pool count could be from zero to integer max count (2147483647) .

* Prefab Reference: The refence is different in each section base on usages.

    * GameObject: The reference of the prefab game object.

    * Component: The reference of the component in object prefab. it is important for getting the exact component main type from pool , assignment should be main type. for example, if the transform component were assigned, getting animation component in code throw exception.

    * Behavior: The reference of the “MonoBehaviour” script which is on object prefab. The assignment should be exact as same as type of class which is use in code or could be just “MonoBehaviour”.

    ![Figure 1](../media/Images/Figure1.png?raw=true)

    There is specific tab for each pool container. after modification of each section the Save button should be apply otherwise changes not applied.

# How To Use
After system setup, the object pooling features could be access by “IPool“ interface. the interface has scheme which provider setup behavior script initialize pipeline, also provide access to three type of pool container and its functionalities.

* SetupBehaviorInitialization: By calling this method all added prefabs in editor which has capability of initialize pipeline, if the pipeline not triggered before. this method automatically calls on pool container when first request rise or could be manually call into program initialize pipeline.
    ```csharp
    void Start()
    {
        _poolSystem.SetupBehaviorInitialization();//manually call
    }
    ```
* GetGameObject: Getting the gameobject instance from gameobject container by unique name key.

* GetGameComponent:  Getting the component from pool component container by unique name key.

* GetGameBehavior: Getting the “MonoBehaviour” instance from behavior container by unique name key.

* AddGameObject: Adding and deactivate the instance to gameobject container by unique name key. log error if specific object pool does not exist.

* AddGameComponent: Adding and deactivate the instance to component container by unique name key. log error if specific object pool does not exist.

* AddGameBehavior: Adding and deactivate the instance to behavior container by unique name key. log error if specific object pool does not exist.

    ```csharp
    public interface IPool
    {
        void SetupBehaviorInitialization();
        T GetGameComponent<T>(string name) where T : Component;
        GameObject GetGameObject(string name);
        T GetGameBehavior<T>(string key) where T : MonoBehaviour;
        void AddGameObject(GameObject item, string name);
        void AddGameBehavior(MonoBehaviour item, string key);
        void AddGameBehavior(IPoolable poolable);
        void AddGameComponent(Component item, string key);
    }
    ```
The IPool reference could be fetch by the user class somehow and desire get method could be used for getting object from pool. if the container has pool count object will retrieve from pooled object, otherwise the new object will instantiate by main prefab reference. object will active before sending out from container.

After working done by received instance, it should return to pool by the code. not backing the object to pool container cause the memory leak. There is interface in system for “MonoBehaviour” objects which could be implemented that notify object code by specific method call when its pop out or push back to pool.

* OnPopOut: Call when object pop out and get out from pull.

* OnPushBack: Call when object push in and go inside pool.

* PoolName: The key name of the prefab which used for retrieving.
    ```csharp
    public interface IPoolable
    {
        string PoolName { get; }
    
        void OnPopOut();
        void OnPushBack();
    }
    ```