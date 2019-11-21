import { PLATFORM } from 'aurelia-pal';

export function configure(config: any) {
  config.globalResources(PLATFORM.moduleName('./AppContent/AppContent'));
}
