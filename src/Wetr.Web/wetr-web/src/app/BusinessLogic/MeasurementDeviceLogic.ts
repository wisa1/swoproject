import { MeasurementDevice } from '../Core';

export abstract class MeasurementDeviceLogic {
  constructor() {}

  private static devicePrefix = 'Device: ';

  public static deviceIsInDashboard(device: MeasurementDevice): boolean {
    return localStorage.getItem(`${this.devicePrefix} ${device.ID.toString()}`) !== null;
  }
  public static invertIsInDashboard(device: MeasurementDevice) {
    if (this.deviceIsInDashboard(device)) {
      console.log('removing...');
      localStorage.removeItem(`${this.devicePrefix} ${device.ID.toString()}`);
    } else {
      console.log('adding...');
      localStorage.setItem(`${this.devicePrefix} ${device.ID.toString()}`, JSON.stringify(device));
    }
  }
}
