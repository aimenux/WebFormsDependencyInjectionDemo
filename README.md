![CI](https://github.com/aimenux/WebFormsDependencyInjectionDemo/workflows/CI/badge.svg)
# WebFormsDependencyInjectionDemo
```
Using dependency injection with legacy webforms
```

In this demo, i m using various ways in order to support ioc with webforms :
- `WebAppUsingUnity` based on unity container (see [link](https://devblogs.microsoft.com/aspnet/use-dependency-injection-in-webforms-application/))
- `WebAppUsingSimpleInjector` based on simple injector container (see [link](https://simpleinjector.readthedocs.io/en/latest/webformsintegration.html))
- `WebAppUsingAutofac` based on autofac container (see [link](https://www.natmarchand.fr/aspnet-dependency-injection-net472/))
- `WebAppUsingServiceCollection` based on microsoft ioc container (see [link](https://www.natmarchand.fr/aspnet-dependency-injection-net472/))

**`Tools`** : vs19, net framework 4.8, webforms