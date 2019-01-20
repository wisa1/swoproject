export * from './devices.service';
import { DevicesService } from './devices.service';
export * from './measurementTypes.service';
import { MeasurementTypesService } from './measurementTypes.service';
export * from './measurements.service';
import { MeasurementsService } from './measurements.service';
export const APIS = [DevicesService, MeasurementTypesService, MeasurementsService];