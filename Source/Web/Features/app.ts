import { PLATFORM } from 'aurelia-pal';
import { Router, RouterConfiguration } from 'aurelia-router';
import './app.scss';

export class App {
  router: any;
  constructor() {}

  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;
    config.map([{ route: '', name: 'Home', moduleId: PLATFORM.moduleName('home') }]);
    config.map([{ route: '/signin', name: 'SignIn', moduleId: PLATFORM.moduleName('Providers/Choosing/SignIn') }]);
    config.map([{ route: '/signin/device', name: 'Device', moduleId: PLATFORM.moduleName('Clients/Device/Authorize') }]);
    config.map([
      { route: '/signin/device/failed', name: 'DeviceFailed', title: 'Device Authorize Failed', moduleId: PLATFORM.moduleName('Clients/Device/Failed') }
    ]);
    config.map([
      { route: '/signin/device/success', name: 'DeviceSuccess', title: 'Device Authorize Succeeded', moduleId: PLATFORM.moduleName('Clients/Device/Success') }
    ]);
    config.map([{ route: '/signin/error', name: 'SignIn', moduleId: PLATFORM.moduleName('SignIn/Error') }]);
    config.map([{ route: '/signin/tenant', name: 'PickTenant', moduleId: PLATFORM.moduleName('Tenants/Choosing/PickTenant') }]);
    config.map([{ route: '/signout', name: 'SignOut', moduleId: PLATFORM.moduleName('SignOut/SignedOut') }]);

    config.mapUnknownRoutes({ redirect: '/' });

    this.router = router;
    console.log(router);
  }
}
