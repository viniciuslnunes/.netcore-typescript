import { toast } from 'react-toastify'

interface Func<T, TResult> {
  (functionParams: T): TResult;
}

interface IHandleErrorsOptions {
  toastMessage?: string;
  hideToast?: boolean;
}

const defaultHandleErrorsOptions: IHandleErrorsOptions = {
  toastMessage: 'Ocorreu um erro',
  hideToast: false
}

export function handleErrors<TParams, TResult> (
  callbackFunction: Func<TParams, TResult>,
  functionParams: TParams,
  options?: IHandleErrorsOptions
): TResult | null {
  options = {
    ...defaultHandleErrorsOptions,
    ...options
  }

  try {
    return callbackFunction(functionParams)
  } catch (error) {
    !options.hideToast && toast.error(options.toastMessage)

    console.warn('Ocorreu um erro.')
    console.error('Ocorreu um erro.', error)

    return null
  }
}
