export interface User {
    id: string;
    name: string,
    email: string
    phomrNumber: string
}

export interface LoginResponse{
    user: User,
    token:string,
}
