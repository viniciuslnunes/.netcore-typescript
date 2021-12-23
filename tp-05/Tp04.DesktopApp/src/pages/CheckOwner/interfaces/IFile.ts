interface IFile extends File {
  isOk: boolean;
  editableContent: string;
  errors: string[]
}

export default IFile
