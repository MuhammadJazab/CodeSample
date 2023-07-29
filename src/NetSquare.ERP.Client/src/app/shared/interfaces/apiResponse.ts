import { ResponseStatus } from "../enums/enums";

export interface ApiResponse {
    status: ResponseStatus;
    message: string;
    resultData: object;
}